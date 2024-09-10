//Calls fetchmovies when the page loads
window.onload = function () {
    fetchMovies();
};

//Fetches movies from Database
async function fetchMovies() {
    try {
        // Make an API call to backend to fetch the movies
        const response = await fetch('https://localhost:7118/api/api/movies');

        if (!response.ok) {
            console.log("Der skete en fejl");
            throw new Error(`Error: ${response.status}`);
        }

        // Parse the JSON response
        const movies = await response.json();

        console.log(movies);

        
        const groupedMovies = groupMoviesByGenre(movies);
 
        console.log(groupedMovies);


        displayMoviesByGenre(groupedMovies);

    } catch (error) {
        // Log any errors that occur during the fetch
        console.error('Error fetching movies:', error);
    }
}

//Groups the movies by genre in new object
function groupMoviesByGenre(movies) {
    return movies.reduce((acc, movie) => {
        if (!acc[movie.genre]) {
            acc[movie.genre] = []; //Adds Genre with empty array
        }
        acc[movie.genre].push(movie);
        return acc;
    }, {});
}

function displayMoviesByGenre(groupedMovies) {
    const moviesContainer = document.getElementById('moviesContainer');
    const placeholderImage = '/Images/placeholder-image.jpg'

    // Loop through each genre
    for (const genre in groupedMovies) {
       // Create and append a genre title
       const genreTitle = document.createElement('h2');
        
       // Get the total number of movies in this genre
       const totalMovies = groupedMovies[genre].length;

       // Set the genre title with the total number of movies
       genreTitle.textContent = `${genre} `;
       
       // Create a span for the total count
       const genreCount = document.createElement('span');
       genreCount.textContent = `(${totalMovies} total)`;
       genreCount.classList.add('genre-count'); 

       // Append the count to the genre title
       genreTitle.appendChild(genreCount);
       genreTitle.classList.add('genre-title'); 
       moviesContainer.appendChild(genreTitle);

        // Create a row for the movies in this genre
        const movieRow = document.createElement('div');
        movieRow.classList.add('movie-row');

        // Get only the first 5 movies
        const firstFiveMovies = groupedMovies[genre].slice(0, 5);

        // Loop through the first 5 movies in this genre and create movie cards
        firstFiveMovies.forEach(movie => {
            // Create the link that wraps the movie item
            const movieLink = document.createElement('a');
            movieLink.href = `movie-details.html?guid=${movie.guid}`; // Use GUID in the URL
            movieLink.classList.add('movie-link');
        
            // Create a movie item (movie card)
            const movieItem = document.createElement('div');
            movieItem.classList.add('movie-item');
        
            // Create and append the movie thumbnail
            const movieThumbnail = document.createElement('img');
            movieThumbnail.src = movie.thumbnailUrl ? movie.thumbnailUrl : '/Images/placeholder-image.jpg';
            movieThumbnail.alt = movie.title || 'Placeholder Image';
            movieItem.appendChild(movieThumbnail);
        
            // Create and append the movie title
            const movieTitle = document.createElement('h3');
            movieTitle.textContent = movie.title;
            movieItem.appendChild(movieTitle);
        
            // Append the movie item inside the movie link
            movieLink.appendChild(movieItem);
        
            // Append the movie link (which wraps the movie item) to the movie row
            movieRow.appendChild(movieLink);
        });

        // Append the movie row to the container
        moviesContainer.appendChild(movieRow);
    }
}




