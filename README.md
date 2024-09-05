# YT-tracker

The YT-tracker project is a server and web application system that allows users to download and track Youtube video playlists. Users access a web page, which communicates with the backend.

# Tech stack

The project consists of a frontend user interface, backend API and database.

- The frontend uses React.js and TypeScript.
- The backend uses C# ASP.NET.
- The database uses MS SQL Server.

| ![Block Diagram](documentation/block%20diagram.drawio.png) | 
|:--:| 
| *Block diagram* |

- Frontend documents
- Backend documents
  - [API routes](documentation/API%20routes.md)
- Database documents

## Development

For local development, only the database is hosted as a docker instance. THe rest are run in development mode natively.

## Deployment

The entire stack will first be developed to work over a LAN network.

## Project management

| ![Product breakdown structure](documentation/product%20breakdown%20structure.drawio.png) | 
|:--:| 
| *Initial product breakdown structure* |

Feature backlog and progress will be tracked in [this GitHub Project](https://github.com/users/shibaholic/projects/3/views/1)

## Initial requirements

| s/n | Name | Description | Priority |
| - | - | - | - |
| 1 | User account | As a user, I want to login/out so that my data is secure. | Must |
| 2 | Playlist | As a user, I want to enter a playlist URL so that I can track it. | Must |
| 3 | See playlist | As a user, I want to see the playlist info, which videos are already downloaded. | Must |
| 4 | Download playlist | As a user, I want to download the playlist so I can store the playlist offline. | Must |
| 5 | Track downloaded videos | As a user, I want to track which videos are already downloaded so I can easily download the newly added videos in the playlist. | Must |
| 6 | Auto playlist update | As a user, I want the server to automatically track newly added videos in the playlist so their metadata is saved on the server. | Must |
| 7 | Track removed videos | As a user, I want the server to track how videos are removed from the playlist. | Must |