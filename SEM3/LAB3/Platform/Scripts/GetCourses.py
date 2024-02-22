from pytube import Playlist, YouTube
import json

URL_PLAYLIST = ""

# Retrieve URLs of videos from playlist
playlist = Playlist(URL_PLAYLIST)

# Create a dictionary to store the information
playlist_info = {
    "playlist_url": URL_PLAYLIST,
    "playlist_title": playlist.title,
    "playlist_description": playlist.description,
    "number_of_videos": len(playlist.video_urls),
    "videos": []
}

# Define a function to get video descriptions
def get_desc(url):
    youtube = YouTube(url)
    stream = youtube.streams.first()
    desc = youtube.description
    return desc

# Iterate through the playlist and gather video information
for url in playlist.video_urls:
    yt = YouTube(url)
    video_info = {
        "video_url": url,
        "video_title": yt.title,
        "video_description": get_desc(url).replace('\n', '<br>')
    }
    playlist_info["videos"].append(video_info)

# Save the information to a JSON file
with open('../Resources/Courses/0.json', 'w', encoding='utf-8') as json_file:
    json.dump(playlist_info, json_file, ensure_ascii=False, indent=4)

print("JSON file '*.json' has been created.")
