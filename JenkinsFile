pipeline {
    agent any

    environment {
        DOTNET_ENV = 'PreProduction'
        PUBLISH_DIR = 'publish'
        DEPLOY_SERVER = 'deployuser@PREPROD_SERVER_IP'
        DEPLOY_PATH = '/var/www/mydotnetapp'
    }

    stages {

        /* ================================
           1️⃣ CHECKOUT (GitHub credentials)
           ================================ */
        stage('Checkout') {
            steps {
                checkout([
                    $class: 'GitSCM',
                    branches: [[name: '*/main']],
                    userRemoteConfigs: [[
                        url: 'git@github.com:mrodriguex/hard.core.git',
                        credentialsId: 'github-ssh'
                    ]]
                ])
            }
        }

        /* ================================
           2️⃣ RESTORE
           ================================ */
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        /* ================================
           3️⃣ BUILD
           ================================ */
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        /* ================================
           4️⃣ PUBLISH
           ================================ */
        stage('Publish') {
            steps {
                sh '''
                  dotnet publish \
                  --configuration Release \
                  --output ${PUBLISH_DIR}
                '''
            }
        }

        /* ==========================================
           5️⃣ DEPLOY (Preprod server credentials)
           ========================================== */
        stage('Deploy to PreProd') {
            steps {
                sshagent(['preprod-ssh']) {
                    sh '''
                      rsync -avz --delete \
                      ${PUBLISH_DIR}/ \
                      ${DEPLOY_SERVER}:${DEPLOY_PATH}

                      ssh ${DEPLOY_SERVER} \
                      "sudo systemctl restart mydotnetapp"
                    '''
                }
            }
        }
    }

    post {
        success {
            echo 'Deployment successful 🚀'
        }
        failure {
            echo 'Deployment failed ❌'
        }
    }
}
