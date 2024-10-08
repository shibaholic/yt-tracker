# API routes

## `/api_v1/`

### User API
| Purpose | Verb | Endpoint | Requested data | Expected response |
|-|-|-|-|-|
| Show logged-in user object | GET | `/user` | JWT auth | 200 User |
| Login | POST | `/login` | Login credentials | 200 User |
| Logout | POST | `/logout` | JWT auth | 200 |
| Signup | POST | `/signup` | Signup credentials | 200 User |

### App API
Requested data always requires the JWT authentication.
| Purpose | Verb | Endpoint | Requested data | Expected response |
|-|-|-|-|-|
| Show all playlists | GET | `/playlists` | | 200 [Playlist] |
| Show playlist | GET | `/playlist/{pl_id}` |  | 200 Playlist |
| Create playlist | POST | `/playlist` | Create playlist form | 200 Playlist location |
| Remove playlist | DELETE | `/playlist/{pl_id}` | | 200 |
| Show videos from playlist | GET | `/playlist/{pl_id}/videos` |  | 200 [Video] |
| Start download session | GET | `/playlist/{pl_id}/download` | Download playlist form | 200 Download Session location |
| Download the completed download session | GET | `/download/{ds_id}/complete` | | 200 zip file |

### App Push API using SignalR
Requested data always requires the JWT authentication.
| Purpose | Method | Endpoint | Requested data | Expected response |
|-|-|-|-|-|
| Show download session progress | OnMessage | `/download/{ds_id}/status` | | 200 Download Session Complete object