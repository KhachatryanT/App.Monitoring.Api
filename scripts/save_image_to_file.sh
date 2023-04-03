#!/bin/bash
mkdir ../publish
docker save -o ../publish/monitoring-api.tar monitoring-api:latest
