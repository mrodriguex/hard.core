pipeline {
    agent any
    
    triggers {
        githubPush()
    }
    
    stages {
        stage('Checkout Code') {
            steps {
                echo '📦 Cloning repository with credentials...'
                
                // ESPECIFICA LAS CREDENCIALES AQUÍ
                checkout([
                    $class: 'GitSCM',
                    branches: [[name: '*/main']],
                    extensions: [
                        [$class: 'CloneOption', depth: 1, shallow: true],
                        [$class: 'CleanBeforeCheckout']
                    ],
                    userRemoteConfigs: [[
                        url: 'https://github.com/mrodriguex/hard.core.git',
                        credentialsId: 'github-token'  // ¡AQUÍ ESTÁ LA CLAVE!
                    ]]
                ])
                
                sh '''
                    echo "✅ Repository cloned successfully!"
                    echo "Branch: $(git branch --show-current)"
                    echo "Latest commit: $(git log -1 --oneline)"
                '''
            }
        }
        
        stage('Build') {
            steps {
                echo '🏗️ Building project...'
                sh '''
                    echo "Listing files:"
                    ls -la
                    echo "Build completed at: $(date)"
                '''
            }
        }
    }
    
    post {
        success {
            echo '🎉 Pipeline completed successfully!'
        }
    }
}
