#!/bin/bash

servicename=$1
fileserver=$2

systemctl stop $"$servicename.service"
wget --no-check-certificate --reject "index.html" --recursive --no-parent -P $servicename -nH --cut-dirs=1 $fileserver/$servicename
rm -r /srv/$servicename
mv $servicename /srv/$servicename
touch $"/etc/systemd/system/$servicename.service"
echo '[Unit]' > $"/etc/systemd/system/$servicename.service"
echo $"Description=$servicename service for ASM" >> $"/etc/systemd/system/$servicename.service"
echo '' >> $"/etc/systemd/system/$servicename.service"
echo '[Service]' >> $"/etc/systemd/system/$servicename.service"
echo 'User=asm-node' >> $"/etc/systemd/system/$servicename.service"
echo 'RestartSec=5' >> $"/etc/systemd/system/$servicename.service"
echo $"ExecStart=/srv/$servicename/$servicename" >> $"/etc/systemd/system/$servicename.service"
echo $"WorkingDirectory=/srv/$servicename" >> $"/etc/systemd/system/$servicename.service"
echo '' >> $"/etc/systemd/system/$servicename.service"
echo '[Install]' >> $"/etc/systemd/system/$servicename.service"
echo 'WantedBy=multi-user.target' >> $"/etc/systemd/system/$servicename.service"
echo '' >> $"/etc/systemd/system/$servicename.service"
chmod -R +x $"/srv/$servicename"
systemctl enable $"$servicename.service"
systemctl start $"$servicename.service"
