#!/bin/bash

docker build -f ./Sources/Referential/Dockerfile ./ -t mlc-accounting-referential

docker-compose up -d

docker exec mlc-accounting-kafka kafka-topics --bootstrap-server mlc-accounting-kafka:9092 --create --topic mlc-accounting-user-integration

exit 0