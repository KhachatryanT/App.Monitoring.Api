docker buildx build -f ../App.Monitoring.Host/Dockerfile --platform linux/amd64,linux/arm64/v8 --output="type=tar,dest=../Publish/monitoring-api.tar" ../
