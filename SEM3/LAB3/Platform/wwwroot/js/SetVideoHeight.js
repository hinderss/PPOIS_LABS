function setVideoHeight() {
    var videoContainer = document.querySelector('.video');

    var width = videoContainer.offsetWidth;
    var height = (width * 9) / 16;

    videoContainer.style.height = height + 'px';
}

function replaceYouTubeURL(url) {
    var regex = /https:\/\/www\.youtube\.com\/watch\?v=([A-Za-z0-9_\-]+)/;
    if (regex.test(url)) {
        var newURL = url.replace(regex, 'https://www.youtube.com/embed/$1');
        return newURL;
    }
    return url;
}

window.addEventListener('resize', setVideoHeight);
window.addEventListener('load', setVideoHeight);