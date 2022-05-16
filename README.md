# Zovies
 Zeds Movie Service


Zovies is a custom movie service that allows you to download and watch any movie available on lookmovies (a movie streaming site)

This application was built for learning purposes (and because i hate movies buffering when im watching them)


## Installation

### Requirements
[OMDB api key](https://www.omdbapi.com/)

### Setup
- clone both this repo and the Zovies-UI repo
- edit the appsettings.json file in Zovies.Backend
- add your api key to the 'OMDBApiKey' field
- add a path to a folder where you want all movies to be downloaded to in the 'SaveFolderPath' field
- edit 'Url' field with the deployment ip address and port




---
## Functionality

Search {Work in ptogress}
- filter downloaded movies based on a genere,rating,year,cast

Download
- Download movies from lookmovie

Choose for me
- select some filters eg. genre  or actor and a random movie will be chosen.
		this is perfect for when you dont know what to watch

## Development

The application is currently developed with svelte on the frontend
and a rest api on the backend developed with c#
[video player to use](https://videojs.com/getting-started)