pipeline {
    agent any
    triggers { githubPush() }
    
    environment {
        SERVER = '192.168.122.138'
        USER = 'manuel'
        APP_PATH = '/home/manuel/www/services/HARD.CORE/HARD.CORE.API'
        SERVICE = 'HARD.CORE.API'
    }
    
    stages {
        stage('Get Code') {
            steps {
                git url: 'https://github.com/mrodriguex/hard.core.git', 
                     credentialsId: 'github-token',
                     branch: 'main'
            }
        }
        
        stage('Build .NET 8') {
            steps {
                sh '''
                    CSPROJ=$(find . -name "*.csproj" | head -1)
                    dotnet publish "$CSPROJ" -c Release -o ./publish --runtime linux-x64
                '''
            }
        }
        
        stage('Deploy') {
            steps {
                sshagent(['deployment_key']) {
                    sh """
                        # Stop service
                        ssh ${USER}@${SERVER} "sudo systemctl stop ${SERVICE}"
                        
                        # Deploy files
                        rsync -avz --delete ./publish/ ${USER}@${SERVER}:${APP_PATH}/
                        
                        # Restart service
                        ssh ${USER}@${SERVER} "
                            sudo chown -R ${USER}:${USER} ${APP_PATH}
                            sudo systemctl daemon-reload
                            sudo systemctl start ${SERVICE}
                            echo 'Service status:'
                            sudo systemctl status ${SERVICE} --no-pager | head -3
                        "
                    """
                }
            }
        }
        
        stage('Verify') {
            steps {
                sshagent(['deployment_key']) {
                    sh """
                        ssh ${USER}@${SERVER} "
                            if systemctl is-active ${SERVICE} >/dev/null; then
                                echo '✅ ${SERVICE} is running'
                                echo '📁 Files in ${APP_PATH}:'
                                ls -la ${APP_PATH}/ | grep -E '(.dll|appsettings)' | head -5
                            else
                                echo '❌ ${SERVICE} failed to start'
                                sudo journalctl -u ${SERVICE} -n 20 --no-pager
                                exit 1
                            fi
                        "
                    """
                }
            }
        }
    }
    
    post {
        success {
            echo '✅ .NET 8 API deployed successfully!'
        }
        failure {
            echo '❌ Deployment failed'
        }
    }
}
