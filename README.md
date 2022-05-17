# Zovies
 Zeds Movie Service


Zovies is a custom movie service that allows you to download and watch any movie available on lookmovies (a movie streaming site)

This application was built for learning purposes (and because i hate movies buffering when im watching them)


## Installation

### Requirements
[OMDB api key](https://www.omdbapi.com/)

[Youtube-dl](https://github.com/ytdl-org/youtube-dl/) installed on your PATH

### Setup
- clone both this repo and the Zovies-UI repo
- edit the appsettings.json file in Zovies.Backend
- add your api key to the 'OMDBApiKey' field
- add a path to a folder where you want all movies to be downloaded to in the 'SaveFolderPath' field
- edit 'Url' field with the deployment ip address and port

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
- select some filters eg. genre or actor and a random movie will be chosen.
		this is perfect for when you dont know what to watch
