#!/bin/bash

docker rm -f $(docker ps -a -q)
docker volume rm $(docker volume ls -q)

#docker image prune -f
#docker container prune -f
#docker volume prune -f

docker build -f ./Sources/Referential/Api/Dockerfile ./ -t mlc-accounting-referential
docker build -f ./Sources/Integration/Api/Dockerfile ./ -t mlc-accounting-integration

docker-compose up -d

docker exec mlc-accounting-kafka kafka-topics --bootstrap-server mlc-accounting-kafka:9092 --create --topic mlc-accounting-user-integration

exit 0