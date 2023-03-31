#!/bin/bash
docker buildx build --platform linux/arm64/v8 -f ../App.Monitoring.Host/Dockerfile -t monitoring-api:latest --load ../
