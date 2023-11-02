function replaceYouTubeURL(url) {
    var regex = /https:\/\/www\.youtube\.com\/watch\?v=([A-Za-z0-9_\-]+)/;
    if (regex.test(url)) {
        var newURL = url.replace(regex, 'https://www.youtube.com/embed/$1');
        return newURL;
    }
    return url;
    }
window.addEventListener('load', function() {
    var url = new URL(window.location.href);
    var id = url.searchParams.get('id');
    var page = url.searchParams.get('page');
    console.log("page: " + page);

    var xhr2 = new XMLHttpRequest();
    xhr2.open('GET', `/getcourse?id=${id}`, true);

    xhr2.onreadystatechange = function () {
        if (xhr2.readyState === 4) {
            if (xhr2.status === 200) {
                var response = JSON.parse(xhr2.responseText);
                console.log(response);

                var courseTopContainer = document.getElementById('coursetopcontainer');
                console.log(courseTopContainer);
                var left = document.getElementsByClassName('left-block')[0];
                console.log(left);
                var right = document.getElementsByClassName('right-block')[0];

                courseTopContainer.innerHTML = '';
                left.innerHTML = '';
                right.innerHTML = '';

                var playlist_url = response.playlist_url;
                var playlist_title = response.playlist_title;
                var playlist_description = response.playlist_description;
                var videos = response.videos;

                courseTopContainer.innerHTML = `
                    <div class="text">
                        <div class="containerheader">
                            <h1 id="name">${playlist_title}</h1>
                        </div>
                        <div class="containerheader">
                            <p id="description">${playlist_description}</p>
                        </div>
                    </div>
                `;
                
                console.log(replaceYouTubeURL(videos[page].video_url));
                console.log(videos[page].video_url);
                left.innerHTML = `
                    <div class="videoblock">
                        <div class="video">
                            <iframe width="100%" height="100%" src="${replaceYouTubeURL(videos[page].video_url)}" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen>
                            </iframe>
                        </div>
                        <h2>${videos[page].video_title}</h2>
                        <p>${videos[page].video_description}</p>
                    </div>
                `;

                videos.forEach(function (video, index) {
                    right.innerHTML += `
                        <a class="videoitem" href="/course?id=${id}&page=${index}">
                            <div>${video.video_title}</div>
                        </a>
                    `;
                });
                setVideoHeight();
            }
            else {
                console.error('Error while requesting /getcourse');
            }
        }
    };

    xhr2.send();    
});