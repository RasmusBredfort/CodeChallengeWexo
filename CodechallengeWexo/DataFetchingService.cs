using CodechallengeWexo.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodechallengeWexo
{
    public class DataFetchingService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiDbContext _dbContext;

        public DataFetchingService(HttpClient httpClient, ApiDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task FetchAndSaveMovies()
        {
            string moviesApiUrl = "https://feed.entertainment.tv.theplatform.eu/f/jGxigC/bb-all-pas?form=json&lang=da&byProgramType=movie";
            var response = await _httpClient.GetAsync(moviesApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var movieList = ExtractMovies(jsonData);

                foreach (var movie in movieList)
                {
                    var existingMovie = _dbContext.Movies.FirstOrDefault(m => m.Title == movie.Title);
                    if (existingMovie == null)
                    {
                        _dbContext.Movies.Add(movie);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task FetchAndSaveSeries()
        {
            string seriesApiUrl = "https://feed.entertainment.tv.theplatform.eu/f/jGxigC/bb-all-pas?form=json&lang=da&byProgramType=series";

            var response = await _httpClient.GetAsync(seriesApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var seriesList = ExtractSeries(jsonData);

                foreach (var series in seriesList)
                {
                    var existingSeries = _dbContext.Series.FirstOrDefault(s => s.Title == series.Title);
                    if (existingSeries == null)
                    {
                        _dbContext.Series.Add(series);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }
        private IEnumerable<Movie> ExtractMovies(string jsonData)
        {
            var movies = new List<Movie>();
            var document = JsonDocument.Parse(jsonData);

            foreach (var item in document.RootElement.GetProperty("entries").EnumerateArray())
            {
                var guid = item.GetProperty("guid").GetString();
                var title = item.GetProperty("title").GetString();
                var description = item.GetProperty("description").GetString();

                var genre = "Unknown"; // Default value for genre
                if (item.TryGetProperty("plprogram$tags", out var tagsArray))
                {
                    foreach (var tag in tagsArray.EnumerateArray())
                    {
                        var scheme = tag.GetProperty("plprogram$scheme").GetString();
                        if (scheme == "genre")
                        {
                            genre = tag.GetProperty("plprogram$title").GetString();
                            break; // Exit the loop after finding the genre
                        }
                    }
                }

                string thumbnailUrl = null;  // Default value for thumbnail URL

              
                if (item.TryGetProperty("plprogram$thumbnails", out var thumbnails))
                {
                    foreach (var thumbnail in thumbnails.EnumerateObject())
                    {
                        // Check if "plprogram$assetTypes" exists in the thumbnail object
                        if (thumbnail.Value.TryGetProperty("plprogram$assetTypes", out var assetTypesArray))
                        {
                            bool isImageType = false;
                            foreach (var assetType in assetTypesArray.EnumerateArray())
                            {
                                if (assetType.GetString() == "Image")
                                {
                                    isImageType = true;
                                    break;  // Exit the loop once we find "Image"
                                }
                            }

                            // If it's an image type, extract the thumbnail URL
                            if (isImageType && thumbnail.Value.TryGetProperty("plprogram$url", out var urlProperty))
                            {
                                thumbnailUrl = urlProperty.GetString();  // Get the URL
                                break;  // Exit loop after finding the first valid image thumbnail
                            }
                        }
                    }
                }


                movies.Add(new Movie
                {
                    Guid = guid,
                    Title = title,
                    Genre = genre,
                    Description = description,
                    ThumbnailUrl = thumbnailUrl,
                });
            }

            return movies;
        }

        private IEnumerable<Series> ExtractSeries(string jsonData)
        {
            var seriesList = new List<Series>();
            var document = JsonDocument.Parse(jsonData);

            foreach (var item in document.RootElement.GetProperty("entries").EnumerateArray())
            {
                var guid = item.GetProperty("guid").GetString();
                var title = item.GetProperty("title").GetString();
                var description = item.GetProperty("description").GetString();

                var genre = "Unknown"; // Default value for genre
                if (item.TryGetProperty("plprogram$tags", out var tagsArray))
                {
                    foreach (var tag in tagsArray.EnumerateArray())
                    {
                        var scheme = tag.GetProperty("plprogram$scheme").GetString();
                        if (scheme == "genre")
                        {
                            genre = tag.GetProperty("plprogram$title").GetString();
                            break; // Exit the loop after finding the genre
                        }
                    }
                }

                seriesList.Add(new Series
                {
                    Guid = guid,
                    Title = title,
                    Genre = genre,
                    Description = description,
                });
            }

            return seriesList;
        }
    }
}
