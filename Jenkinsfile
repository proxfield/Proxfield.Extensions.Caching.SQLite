pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet build --no-restore --configuration Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test --no-build --configuration Release'
            }
        }
    }
}
