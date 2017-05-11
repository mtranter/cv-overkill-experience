set -e
cd src/ui
gulp test
gulp bundle
cd ./../../deploy
terraform plan -out=./tf-deploy-plan
terraform apply ./tf-deploy-plan
