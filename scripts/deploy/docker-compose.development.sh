#!/bin/bash
docker load --input monitoring-api.tar
docker load --input monitoring-webui.tar
docker-compose -f development/docker-compose.yml up -d
