document.addEventListener('DOMContentLoaded', function () {
    const userId = '@User.FindFirstValue("UserId")';

    async function searchMovie() {
        const title = document.getElementById('searchInput').value;
        if (!title) return;

        const response = await fetch(`/Home/Get?title=${encodeURIComponent(title)}`);
        const movieInfoDiv = document.getElementById('movieInfo');

        if (response.ok) {
            const data = await response.json();
            movieInfoDiv.innerHTML = `
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-md-4">
                            <img src="${data.poster}" alt="${data.title}" class="img-fluid" />
                        </div>
                        <div class="col-md-8">
                            <h3>${data.title}</h3>
                            <p><strong>IMDB Rating:</strong> ${data.imdbRating}</p>
                            <p><strong>Year:</strong> ${data.year}</p>
                            <p><strong>Genre:</strong> ${data.genre}</p>
                            <p><strong>Runtime:</strong> ${data.runtime}</p>
                            <p><strong>Director:</strong> ${data.director}</p>
                            <p><strong>Actors:</strong> ${data.actors}</p>
                            <p><strong>Plot:</strong></p>
                            <p>${data.plot}</p>
                            <button id="saveButton" class="btn btn-success">Save</button>
                            <div id="saveMessage" class="mt-2" style="display:none;"></div>
                        </div>
                    </div>
                </div>
            `;

            document.getElementById('saveButton').addEventListener('click', async function () {
                const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

                const saveResponse = await fetch('/Home/Save', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': token
                    },
                    body: JSON.stringify({
                        Title: data.title,
                        Genre: data.genre,
                        Year: data.year,
                        Runtime: data.runtime,
                        ImdbRating: data.imdbRating,
                        Director: data.director,
                        Actors: data.actors,
                        Plot: data.plot,
                        Poster: data.poster,
                        UserId: userId
                    })
                });

                const messageDiv = document.getElementById('saveMessage');
                console.log("Error response received:", errorResponse);
                if (saveResponse.ok) {
                    const responseJson = await saveResponse.json();
                    messageDiv.style.display = 'block';
                    messageDiv.style.color = 'green';
                    messageDiv.textContent = responseJson.message;
                } else {
                    const errorResponse = await saveResponse.json();
                    messageDiv.style.display = 'block';
                    messageDiv.style.color = 'red';

                    if (errorResponse.message === "User not logged in.") {
                        messageDiv.innerHTML = `Failed to save movie: ${errorResponse.message} <a href="Identity/Account/Login">Log in</a>`;
                    } else if (errorResponse.message === "This object already exists in your list.") {
                        // Ingen logga-in-länk här
                        messageDiv.textContent = `Failed to save movie: ${errorResponse.message}`;
                    } else {
                        messageDiv.textContent = `Failed to save movie: ${errorResponse.message || 'An unknown error occurred.'}`;
                    }
                }
            });

        } else {
            movieInfoDiv.innerHTML = `<p class="text-danger">Movie not found.</p>`;
        }
    }

    document.getElementById('searchButton').addEventListener('click', function () {
        searchMovie();
    });

    document.getElementById('searchInput').addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            searchMovie();
        }
    });
});