#!/bin/bash
docker load --input monitoring-api.tar
docker load --input monitoring-webui.tar
docker-compose -f production/docker-compose.yml up -d