window.addEventListener('load', function() {
    var tag = "Popular"

    var xhr = new XMLHttpRequest();
    xhr.open('GET', `/gettagcourses?tag=${tag}`, true);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                console.log(response);

                var courseContainer = document.getElementsByClassName('centered-container')[0];

                courseContainer.innerHTML = '';

                response.forEach(function (course) {
                    course = JSON.parse(course);
                    courseContainer.innerHTML += `
                        <a href="/buycourse?id=${course.id}" class="course-link">
                            <div class="course">
                                <p>курс</p>
                                <p>•</p>
                                <p class="course-price">${course.price}$</p>
                                <h2 class="course-name">${course.name}</h2>
                                <p class="course-description">${course.description}</p>
                                <br>
                                <p class="course-duration">${course.duration} серий</p>
                            </div>
                        </a>
                    `;
                });
            }
            else {
                console.error('Ошибка при запросе /getallcourses');
            }
        }
    };

    xhr.send();
});