#!/bin/bash
cd /usr/local/codedeployresources/
LOG_FILE="/usr/local/codedeployresources/deploy.log"

echo "--- Deploy date: $(date '+%d/%m/%Y %H:%M:%S') ---" >> $LOG_FILE
echo "-> Pulling latest images..." >> $LOG_FILE
sudo docker compose pull engexam >> $LOG_FILE 2>&1
echo "-> Clean container..." >> $LOG_FILE
sudo docker rm -f engexam >> $LOG_FILE 2>&1
sudo docker compose up -d --force-recreate engexam >> $LOG_FILE 2>&1
sudo docker image prune -f >> $LOG_FILE 2>&1

echo "-> Done!" >> $LOG_FILE