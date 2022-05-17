# Zovies
 Zeds Movie Service


Zovies is a custom movie service that allows you to download and watch any movie available on lookmovies (a movie streaming site)

This application was built for learning purposes (and because i hate movies buffering when im watching them)


## Installation

### Requirements
[OMDB api key](https://www.omdbapi.com/)

[Youtube-dl](https://github.com/ytdl-org/youtube-dl/) installed on your PATH

chromium installed

[install dotnet](https://docs.microsoft.com/en-us/dotnet/iot/deployment)
```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
```

install dotnet ef: `dotnet tool install --global dotnet-ef`



### Setup
- clone both this repo
- find the IP adress of this device with `ifconfig`
- change directories into Zovies.Backend
- edit the appsettings.json file
- add your api key to the 'OMDBApiKey' field
- add a path to a folder where you want all movies to be downloaded to in the 'SaveFolderPath' field
- edit 'Url' field with the deployment ip address and port set to `8080` eg. address `http://192.168.1.1:8080`
- edit the 'CorsUrl' field with the same address minus the port i.e `http://192.168.1.1`

### Setup Database
install sqlite3 `sudo apt install sqlite3`

run `dotnet ef database update` this will create the applications database and run the migrations to generate the tables

### Deploying application

to create a build of the backend, make sure you are in `Zovies.Backend`

run `dotnet publish -o build` to build the project and output to a build folder

change directory to build folder (`cd build`)

run `dotnet Zovies.Backend.dll` to start the server

if you have deployed the UI then you are ready to start downloading and streaming movies on your local network!



---

## How to download movies?
- select movie on [lookmovie.ag](https://lookmovie.ag/) (you will most likely be redirected to another lookmovie site
that's fine)
- Navigate to the player page (where you watch the movie)
- copy url from address bar
- navigate to zovies and click on download link
- paste url into text field and click download
- wait a moment until you see a small notification in the bottom left corner
  - if notification mentions 'the movie couldn't be found' then there was no HD resolution video available :(
- otherwise wait until the movie has finished being downloaded (~10 minutes)



## Functionality

Search {Work in ptogress}
- filter downloaded movies based on a genere,rating,year,cast

Download
- Download movies from lookmovie

Choose for me
- selects a random movie for you to watch
