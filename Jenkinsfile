pipeline {
    agent any
    triggers { githubPush() }
    
    environment {
        DEPLOY_PATH = '/home/manuel/www/services/HARD.CORE/HARD.CORE.API'
    }
    
    stages {
        stage('Prepare Deployment') {
            steps {
                sshagent(['deployment_key']) {
                    script {
                        // Verificar si el directorio existe remotamente
                        def dirExists = sh(
                            script: """
                                ssh manuel@192.168.122.138 "
                                    if [ -d '${DEPLOY_PATH}' ]; then
                                        echo 'EXISTS'
                                    else
                                        echo 'NOT_EXISTS'
                                    fi
                                "
                            """,
                            returnStdout: true
                        ).trim()
                        
                        if (dirExists == 'NOT_EXISTS') {
                            echo "Creando directorio ${DEPLOY_PATH}..."
                            sh """
                                ssh manuel@192.168.122.138 "
                                    sudo mkdir -p ${DEPLOY_PATH}
                                    sudo chown -R manuel:manuel ${DEPLOY_PATH}
                                    echo 'Directorio creado'
                                "
                            """
                        }
                    }
                }
            }
        }
        
        stage('Deploy Application') {
            steps {
                sshagent(['deployment_key']) {
                    sh """
                        # Stop service
                        ssh manuel@192.168.122.138 "sudo systemctl stop HARD.CORE.API 2>/dev/null || echo 'Service not running'"
                        
                        # Deploy with rsync (create directories automatically)
                        rsync -avz --delete --rsync-path="sudo rsync" \
                            ./publish/ \
                            manuel@192.168.122.138:${DEPLOY_PATH}/
                        
                        # Fix permissions and start
                        ssh manuel@192.168.122.138 "
                            sudo chown -R manuel:manuel ${DEPLOY_PATH}
                            sudo chmod -R 755 ${DEPLOY_PATH}
                            sudo systemctl daemon-reload
                            sudo systemctl start HARD.CORE.API
                        "
                    """
                }
            }
        }
    }
}
