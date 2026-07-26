#!/bin/bash
cd /home/ssm-user/engexam/
LOG_FILE="/home/ssm-user/engexam/deploy.log"

echo "--- Deploy date: $(date '+%d/%m/%Y %H:%M:%S') ---" >> $LOG_FILE

echo "-> Fetching latest configuration from AWS Parameter Store..." >> $LOG_FILE
aws ssm get-parameters-by-path \
  --path "/engexam/" \
  --with-decryption \
  --region ap-southeast-1 \
  --query "Parameters[*].[Name,Value]" \
  --output text | sed 's|/engexam/||' | sed 's/\t/=/' > .env.backend 2>> $LOG_FILE

echo "-> Pulling latest images..." >> $LOG_FILE
sudo docker pull pdtuan04/engexam:latest >> $LOG_FILE 2>&1

echo "-> Clean container..." >> $LOG_FILE
sudo docker rm -f engexam >> $LOG_FILE 2>&1

sudo docker run -d --name engexam --restart unless-stopped --env-file .env.backend -p 8080:8080 pdtuan04/engexam:latest >> $LOG_FILE 2>&1

sudo docker image prune -f >> $LOG_FILE 2>&1

echo "-> Done!" >> $LOG_FILE