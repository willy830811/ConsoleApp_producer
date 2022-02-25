pipeline {
    agent any
    triggers {
        githubPush()
    }
    stages {
        stage('Restore packages'){
           steps{
               sh 'dotnet restore ConsoleApp_consumer.sln'
            }
         }
        stage('Clean'){
           steps{
               sh 'dotnet clean ConsoleApp_consumer.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build ConsoleApp_consumer.sln --configuration Release'
            }
         }
        stage('Publish'){
             steps{
               sh 'dotnet publish ConsoleApp_consumer.csproj --configuration Release'
             }
        }
    }
}