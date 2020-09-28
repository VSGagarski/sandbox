$remote=$args[0]
$service=$args[1]

scp -pr .\build_service.sh ${remote}:~/
ssh -t -t ${remote} "su -c 'chmod +x build_service.sh'"
ssh -t -t ${remote} "su -c './build_service.sh ${service} https://fileserver.auto-sys.su'"
