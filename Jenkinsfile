pipeline {
    agent any
    triggers {
        githubPush()
    }
    stages {
        stage('Restore packages'){
           steps{
               sh 'dotnet restore ConsoleApp_producer.sln'
            }
         }
        stage('Clean'){
           steps{
               sh 'dotnet clean ConsoleApp_producer.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build ConsoleApp_producer.sln --configuration Release'
            }
         }
        stage('Publish'){
             steps{
               sh 'dotnet publish ConsoleApp_producer.csproj --configuration Release'
             }
        }
    }
}