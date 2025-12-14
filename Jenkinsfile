pipeline {
    agent any
    
    triggers {
        githubPush()  // Se activa automáticamente con cada push
    }
    
    environment {
        DEPLOY_SERVER = '192.168.122.138'
        DEPLOY_USER = 'manuel'
        APP_NAME = 'HARD.CORE.API'
        SERVICE_NAME = 'HARD.CORE.API'
        DEPLOY_PATH = '/home/manuel/www/services/HARD.CORE/HARD.CORE.API'
        DOTNET_VERSION = '8.0'
    }
    
    stages {
        stage('Checkout Code') {
            steps {
                echo '📦 Cloning .NET 8 repository...'
                checkout([
                    $class: 'GitSCM',
                    branches: [[name: '*/main']],
                    extensions: [
                        [$class: 'CloneOption', depth: 1, shallow: true],
                        [$class: 'CleanBeforeCheckout']
                    ],
                    userRemoteConfigs: [[
                        url: 'https://github.com/mrodriguex/hard.core.git',
                        credentialsId: 'github-token'
                    ]]
                ])
                
                sh '''
                    echo "✅ Repository: HARD.CORE.API"
                    echo "🌿 Branch: $(git branch --show-current)"
                    echo "🔑 Commit: $(git log -1 --oneline)"
                    echo "👤 Author: $(git log -1 --pretty=format:'%an')"
                    echo "📁 .NET Solution files:"
                    find . -name "*.sln" -o -name "*.csproj" | head -5
                '''
            }
        }
        
        stage('Build .NET 8') {
            steps {
                echo '🏗️ Building .NET 8 application...'
                sh '''
                    # Verificar estructura del proyecto
                    echo "=== Project Structure ==="
                    ls -la
                    
                    # Buscar archivo .sln o .csproj
                    SLN_FILE=$(find . -name "*.sln" | head -1)
                    CSPROJ_FILE=$(find . -name "*.csproj" | head -1)
                    
                    if [ -n "$SLN_FILE" ]; then
                        echo "Building solution: $SLN_FILE"
                        dotnet build "$SLN_FILE" --configuration Release
                    elif [ -n "$CSPROJ_FILE" ]; then
                        echo "Building project: $CSPROJ_FILE"
                        dotnet build "$CSPROJ_FILE" --configuration Release
                    else
                        echo "❌ No .sln or .csproj files found!"
                        exit 1
                    fi
                    
                    echo "✅ Build completed successfully"
                '''
            }
        }
        
        stage('Publish .NET 8') {
            steps {
                echo '📦 Publishing .NET 8 application...'
                sh '''
                    # Crear directorio de publicación
                    rm -rf publish
                    mkdir -p publish
                    
                    # Buscar proyecto principal
                    CSPROJ_FILE=$(find . -name "*.csproj" | head -1)
                    if [ -z "$CSPROJ_FILE" ]; then
                        echo "❌ No .csproj file found!"
                        exit 1
                    fi
                    
                    echo "Publishing project: $CSPROJ_FILE"
                    
                    # Publicar para producción
                    dotnet publish "$CSPROJ_FILE" \
                        --configuration Release \
                        --output ./publish \
                        --self-contained false \
                        --runtime linux-x64 \
                        --no-restore
                    
                    echo "📊 Publish output:"
                    ls -lh ./publish/
                    du -sh ./publish/
                '''
            }
        }
        
        stage('Stop Service & Backup') {
            steps {
                echo '🔄 Stopping service and creating backup...'
                
                sshagent(['deployment_key']) {
                    sh """
                        ssh -o StrictHostKeyChecking=no ${DEPLOY_USER}@${DEPLOY_SERVER} "
                            echo '=== PRE-DEPLOYMENT PREPARATION ==='
                            
                            # 1. Detener el servicio
                            echo '⏸️  Stopping ${SERVICE_NAME} service...'
                            sudo systemctl stop ${SERVICE_NAME} || echo 'Service was not running'
                            sleep 2
                            
                            # 2. Crear backup si existe despliegue anterior
                            if [ -d '${DEPLOY_PATH}' ]; then
                                echo '💾 Creating backup...'
                                BACKUP_DIR='${DEPLOY_PATH}.backup.\$(date +%Y%m%d_%H%M%S)'
                                sudo cp -r ${DEPLOY_PATH} "\$BACKUP_DIR"
                                echo "Backup created: \$BACKUP_DIR"
                                
                                # Mantener solo últimos 5 backups
                                sudo ls -td ${DEPLOY_PATH}.backup.* 2>/dev/null | tail -n +6 | xargs -r sudo rm -rf
                            else
                                echo '📁 No previous deployment found'
                                sudo mkdir -p ${DEPLOY_PATH}
                                sudo chown -R ${DEPLOY_USER}:${DEPLOY_USER} ${DEPLOY_PATH}
                            fi
                            
                            # 3. Limpiar directorio de despliegue
                            echo '🧹 Cleaning deployment directory...'
                            sudo rm -rf ${DEPLOY_PATH}/*
                            
                            echo '✅ Preparation completed'
                        "
                    """
                }
            }
        }
        
        stage('Deploy Files') {
            steps {
                echo '🚀 Deploying files to server...'
                
                sshagent(['deployment_key']) {
                    sh """
                        echo "📤 Deploying to ${DEPLOY_PATH}"
                        
                        # 1. Sincronizar archivos publicados
                        rsync -avz -e "ssh -o StrictHostKeyChecking=no" \
                            --progress \
                            --delete \
                            ./publish/ ${DEPLOY_USER}@${DEPLOY_SERVER}:${DEPLOY_PATH}/
                        
                        # 2. Copiar archivos de configuración si existen
                        if [ -f "appsettings.Production.json" ]; then
                            echo "📄 Copying production configuration..."
                            scp -o StrictHostKeyChecking=no \
                                appsettings.Production.json \
                                ${DEPLOY_USER}@${DEPLOY_SERVER}:${DEPLOY_PATH}/appsettings.json
                        fi
                        
                        # 3. Asegurar permisos
                        ssh -o StrictHostKeyChecking=no ${DEPLOY_USER}@${DEPLOY_SERVER} "
                            echo '🔐 Setting permissions...'
                            sudo chown -R ${DEPLOY_USER}:${DEPLOY_USER} ${DEPLOY_PATH}
                            sudo chmod -R 755 ${DEPLOY_PATH}
                            
                            echo '📁 Final directory structure:'
                            ls -la ${DEPLOY_PATH}/
                            echo ''
                            echo '📦 File count:'
                            find ${DEPLOY_PATH} -type f | wc -l
                        "
                    """
                }
            }
        }
        
        stage('Update Systemd Service') {
            steps {
                echo '⚙️ Updating systemd service configuration...'
                
                sshagent(['deployment_key']) {
                    sh """
                        # Verificar y actualizar service file si existe en el repo
                        if [ -f "${SERVICE_NAME}.service" ]; then
                            echo "🔄 Updating systemd service file..."
                            scp -o StrictHostKeyChecking=no \
                                ${SERVICE_NAME}.service \
                                ${DEPLOY_USER}@${DEPLOY_SERVER}:/tmp/${SERVICE_NAME}.service.new
                            
                            ssh -o StrictHostKeyChecking=no ${DEPLOY_USER}@${DEPLOY_SERVER} "
                                echo 'Comparing service files...'
                                if ! sudo cmp -s /tmp/${SERVICE_NAME}.service.new /etc/systemd/system/${SERVICE_NAME}.service; then
                                    echo 'Service file changed, updating...'
                                    sudo cp /tmp/${SERVICE_NAME}.service.new /etc/systemd/system/${SERVICE_NAME}.service
                                    sudo systemctl daemon-reload
                                    echo '✅ Service file updated and daemon reloaded'
                                else
                                    echo 'Service file unchanged'
                                fi
                                sudo rm -f /tmp/${SERVICE_NAME}.service.new
                            "
                        else
                            echo "📝 Using existing systemd service: /etc/systemd/system/${SERVICE_NAME}.service"
                        fi
                    """
                }
            }
        }
        
        stage('Start Service') {
            steps {
                echo '▶️ Starting .NET 8 service...'
                
                sshagent(['deployment_key']) {
                    sh """
                        ssh -o StrictHostKeyChecking=no ${DEPLOY_USER}@${DEPLOY_SERVER} "
                            echo '🚀 Starting ${SERVICE_NAME} service...'
                            
                            # Recargar systemd
                            sudo systemctl daemon-reload
                            
                            # Habilitar para inicio automático
                            sudo systemctl enable ${SERVICE_NAME}
                            
                            # Iniciar servicio
                            sudo systemctl start ${SERVICE_NAME}
                            
                            # Esperar y verificar estado
                            sleep 3
                            echo '📊 Service status:'
                            sudo systemctl status ${SERVICE_NAME} --no-pager | head -10
                            
                            # Verificar que el proceso está corriendo
                            echo '🔍 Checking running processes:'
                            ps aux | grep -E '(dotnet|${APP_NAME})' | grep -v grep || echo 'No process found yet'
                        "
                    """
                }
            }
        }
        
        stage('Health Check') {
            steps {
                echo '🏥 Performing health check...'
                
                sshagent(['deployment_key']) {
                    script {
                        // Intentar obtener el puerto del servicio
                        def port = sh(
                            script: """
                                ssh ${DEPLOY_USER}@${DEPLOY_SERVER} "
                                    # Intentar obtener puerto del servicio
                                    sudo systemctl show ${SERVICE_NAME} -p ExecStart | grep -o ':[0-9]\+' | head -1 | tr -d ':' || echo '5000'
                                "
                            """,
                            returnStdout: true
                        ).trim()
                        
                        echo "Testing health check on port: ${port}"
                        
                        // Health check con timeout
                        timeout(time: 30, unit: 'SECONDS') {
                            sh """
                                # Intentar conectarse al servicio
                                if ssh ${DEPLOY_USER}@${DEPLOY_SERVER} "
                                    timeout 10 curl -f http://localhost:${port}/health || \
                                    timeout 10 curl -f http://localhost:${port}/ || \
                                    timeout 10 curl -f http://localhost:${port}/swagger || \
                                    echo 'HEALTH_CHECK_FAILED'
                                " | grep -v "HEALTH_CHECK_FAILED"; then
                                    echo "✅ Health check PASSED on port ${port}"
                                else
                                    echo "❌ Health check FAILED on port ${port}"
                                    # Mostrar logs del servicio para debugging
                                    ssh ${DEPLOY_USER}@${DEPLOY_SERVER} "
                                        echo '=== SERVICE LOGS ==='
                                        sudo journalctl -u ${SERVICE_NAME} --since '5 minutes ago' --no-pager | tail -20
                                    "
                                    exit 1
                                fi
                            """
                        }
                    }
                }
            }
        }
        
        stage('Final Verification') {
            steps {
                echo '🔍 Final deployment verification...'
                
                sshagent(['deployment_key']) {
                    sh """
                        ssh -o StrictHostKeyChecking=no ${DEPLOY_USER}@${DEPLOY_SERVER} "
                            echo '=== FINAL DEPLOYMENT VERIFICATION ==='
                            echo ''
                            echo '📊 Service Status:'
                            sudo systemctl is-active ${SERVICE_NAME} && echo '✅ ACTIVE' || echo '❌ INACTIVE'
                            echo ''
                            echo '📈 Uptime:'
                            sudo systemctl status ${SERVICE_NAME} | grep -o 'Active:.*' | head -1
                            echo ''
                            echo '💾 Disk Usage:'
                            du -sh ${DEPLOY_PATH}
                            echo ''
                            echo '📁 Deployment Files:'
                            ls -la ${DEPLOY_PATH}/ | grep -E '(.dll|.exe|appsettings)' | head -10
                            echo ''
                            echo '🌐 Listening Ports:'
                            sudo netstat -tlnp | grep dotnet || echo 'No dotnet process listening'
                            echo ''
                            echo '✅ Deployment verification completed'
                        "
                    """
                }
            }
        }
    }
    
    post {
        success {
            echo '🎉 .NET 8 DEPLOYMENT SUCCESSFUL!'
            sh '''
                echo "=========================================="
                echo "✅ HARD.CORE.API DEPLOYMENT COMPLETE"
                echo "=========================================="
                echo "Application: ${APP_NAME}"
                echo "Server: ${DEPLOY_USER}@${DEPLOY_SERVER}"
                echo "Path: ${DEPLOY_PATH}"
                echo "Service: ${SERVICE_NAME}.service"
                echo "Build: #${BUILD_NUMBER}"
                echo "Commit: $(git log -1 --oneline)"
                echo "Time: $(date)"
                echo "=========================================="
            '''
            
            // Opcional: Notificar a Slack/Teams/Email
            // emailext to: 'team@example.com', subject: 'Deployment Successful', body: '...'
        }
        
        failure {
            echo '❌ .NET 8 DEPLOYMENT FAILED!'
            sh '''
                echo "=== DEPLOYMENT FAILED ==="
                echo "Check Jenkins logs for details"
                echo "Last stage: ${currentBuild.currentResult}"
                echo "Consider rolling back if necessary"
            '''
            
            // Rollback automático opcional
            sshagent(['deployment_key']) {
                sh '''
                    echo "Attempting automatic rollback..."
                    ssh ${DEPLOY_USER}@${DEPLOY_SERVER} "
                        LATEST_BACKUP=\$(ls -td ${DEPLOY_PATH}.backup.* 2>/dev/null | head -1)
                        if [ -n "\$LATEST_BACKUP" ]; then
                            echo "Rolling back to: \$LATEST_BACKUP"
                            sudo systemctl stop ${SERVICE_NAME}
                            sudo rm -rf ${DEPLOY_PATH}/*
                            sudo cp -r "\$LATEST_BACKUP"/* ${DEPLOY_PATH}/
                            sudo chown -R ${DEPLOY_USER}:${DEPLOY_USER} ${DEPLOY_PATH}
                            sudo systemctl start ${SERVICE_NAME}
                            echo "✅ Rollback completed"
                        else
                            echo "❌ No backup found for rollback"
                        fi
                    "
                '''
            }
        }
        
        always {
            echo '🧹 Cleaning workspace...'
            cleanWs()
            
            // Guardar artefactos de build
            archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
        }
    }
}
