pipeline {
    agent any
    triggers { githubPush() }
    
    stages {
        stage('Build & Deploy') {
            steps {
                git url: 'https://github.com/mrodriguex/hard.core.git', 
                     credentialsId: 'github-token',
                     branch: 'main'
                
                sh 'dotnet publish -c Release -o ./publish --runtime linux-x64'
                
                sshagent(['deployment_key']) {
                    sh '''
                        # SOLUCIÓN: Crear directorios como usuario normal (sin sudo)
                        ssh manuel@192.168.122.138 "
                            mkdir -p /home/manuel/www/services/HARD.CORE/HARD.CORE.API
                            echo 'Directorio creado (sin sudo)'
                        "
                        
                        # Parar servicio (esto SÍ necesita sudo, pero debería funcionar con nuestra configuración)
                        ssh manuel@192.168.122.138 "sudo systemctl stop HARD.CORE.API || echo 'Service not running'"
                        
                        # Sincronizar archivos
                        rsync -avz --delete ./publish/ manuel@192.168.122.138:/home/manuel/www/services/HARD.CORE/HARD.CORE.API/
                        
                        # Iniciar servicio
                        ssh manuel@192.168.122.138 "
                            sudo systemctl daemon-reload
                            sudo systemctl start HARD.CORE.API
                        "
                    '''
                }
            }
        }
    }
}
