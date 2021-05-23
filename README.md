# System Auth
This project publishes the jwt for users login, and controls the autherization of members who visit in different pages while browsing [Farmer Web](https://github.com/TzyHuan/FarmerWeb).

## How to use
* Sign in
    - Call `/api/Auth/Authenticate` with account/password to revieve a jwt. (It had better have encryption or https)
    - Call `/api/System/GetAllowedMenu` with the jwt header to get pages can be browsed by this user.
    - The front-end dynamic render the pages through the list of allow menu.
* Sign up
    - not implement yet.
* Adjust authorization levels, pages, etc
    - CURD the controllers of `Actions`, `Ctrls`, `Menus`, `RoleGroups`...
    - As long as any requests are recieved, the filter will check whether the jwt is valid or not.
## Deploy
### Prepare

[Install .NET on Linux](https://docs.microsoft.com/zh-tw/dotnet/core/install/linux)

### Set Linux service
1. Create a config
    ```bash
    sudo nano /etc/systemd/system/system-auth.service
    ```
2. Config sample:
    ```ini
    [Unit]
    Description= SystemAuth Web API App running on Raspberry pi

    [Service]
    WorkingDirectory=/home/pi/IoT/SystemAuth
    ExecStart=/opt/dotnet/dotnet /home/pi/IoT/SystemAuth/SystemAuth.dll
    Restart=always
    # Restart service after 10 seconds if the dotnet service crashes:
    RestartSec=10
    SyslogIdentifier=dotnet-example
    User=www-data
    Environment=ASPNETCORE_ENVIRONMENT=Production
    Envirionment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

    [Install]
    WantedBy=multi-user.target
    ```
3. Enable and run the service:
    ```bash
    sudo systemctl enable system-auth.service
    sudo systemctl start system-auth.service
    ```