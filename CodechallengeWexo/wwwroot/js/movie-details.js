// Fetch movie GUID from the URL
const urlParams = new URLSearchParams(window.location.search);
const movieGuid = urlParams.get('guid');

// Function to fetch all movies from Database
async function fetchAllMovies() {
    try {
        //Make an API call to the backend to fetch the movies
        const response = await fetch('https://localhost:7118/api/api/movies');

        if (!response.ok) {
            throw new Error(`Failed to fetch movies: ${response.status}`);
        }

        //Parse the JSON response
        const movies = await response.json();

        return movies;

    } catch (error) {
        console.error('Error fetching movies:', error);
        document.getElementById('movieDetails').innerText = 'Movies not found.'; //If movie is not found when trying to fetch it
    }
}

// Function to find a specific movie by GUID and display it
async function fetchMovieByGuid(guid) {
    try {
        // Fetch all movies first
        const movies = await fetchAllMovies();

        // Find the movie with the matching GUID
        const movie = movies.find(m => m.guid === guid);

        // If the movie is not found, display an error
        if (!movie) {
            throw new Error('Movie not found');
        }

        // If the movie is found, display its details
        displayMovieDetails(movie);

    } catch (error) {
        console.error('Error fetching movie:', error);
        document.getElementById('movieDetails').innerText = 'Movie not found.';
    }
}

function displayMovieDetails(movie) {
    const movieDetailsSection = document.getElementById('movieDetails');
    
    // Clear previous content
    movieDetailsSection.innerHTML = '';

    //Create and append movie title
    const movieTitle = document.createElement('h1');
    movieTitle.textContent = movie.title;
    movieTitle.classList.add('movie-details-title');
    movieDetailsSection.appendChild(movieTitle);

    //Create and append movie thumbnail
    const movieThumbnail = document.createElement('img');
    movieThumbnail.src = movie.thumbnailUrl || '/Images/placeholder-image.jpg';
    movieThumbnail.alt = movie.title;
    movieThumbnail.classList.add('movie-details-thumbnail');
    movieDetailsSection.appendChild(movieThumbnail);

    //Create and append description label
    const descriptionLabel = document.createElement('h2');
    descriptionLabel.textContent = 'Description:';
    descriptionLabel.classList.add('movie-details-description-label'); 
    movieDetailsSection.appendChild(descriptionLabel);

    //Create and append movie description
    const movieDescription = document.createElement('p');
    movieDescription.textContent = movie.description;
    movieDescription.classList.add('movie-details-description');
    movieDetailsSection.appendChild(movieDescription);
}

// Fetch and display the movie when the page loads
fetchMovieByGuid(movieGuid);