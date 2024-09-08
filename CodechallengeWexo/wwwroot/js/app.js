window.onload = function () {
    fetchMovies();
};

//Fetches movies from Database
async function fetchMovies() {
    try {
        // Make an API call to backend to fetch the movies
        const response = await fetch('http://localhost:5089/api/api/movies');

        if (!response.ok) {
            console.log("Der skete en fejl");
            throw new Error(`Error: ${response.status}`);
        }

        // Parse the JSON response
        const movies = await response.json();

        console.log(movies);

        const groupedMovies = groupMoviesByGenre(movies);

        console.log(groupedMovies);

    } catch (error) {
        // Log any errors that occur during the fetch
        console.error('Error fetching movies:', error);
    }
}

function groupMoviesByGenre(movies) {
    return movies.reduce((acc, movie) => {
        if (!acc[movie.genre]) {
            acc[movie.genre] = []; //Adds Genre with empty array
        }
        acc[movie.genre].push(movie);
        return acc;
    }, {});
}




