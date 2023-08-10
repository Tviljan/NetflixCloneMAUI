using NetflixCloneMAUI.Models;

namespace NetflixCloneMAUI.Services
{
    public class TmdbService
    {
        
        private static List<Media> _fakeMovies = Enumerable.Range(1, 10).Select(i => new Media
        {
            DisplayTitle = $"Fake Movie {i}",
            Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris commodo, massa a ultricies dapibus, mi tortor egestas sem, a accumsan elit tellus eget magna. Nulla facilisi. Suspendisse potenti. Nam ut aliquet velit. Curabitur eget risus auctor, sagittis libero quis, aliquet odio. Nulla facilisi. Nulla facilisi. Morbi sed libero quis ex cursus blandit. In hac habitasse platea dictumst. Maecenas ut nisl velit. Phasellus id nisl euismod, vehicula est a, suscipit nisl. Proin sit amet velit id risus sagittis convallis. Donec in tortor quis eros aliquet aliquet. Vivamus accumsan nisl eu tellus blandit, eget ultrices sapien ultricies. Fusce eget posuere ante, nec vulputate sapien.",
            MediaType = "movie",
            ReleaseDate = "2021-01-01",
            Thumbnail = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/9HT9982bzgN5on1sLRmc1GMn6ZC.jpg",
            ThumbnailSmall = "https://image.tmdb.org/t/p/w220_and_h330_face/9HT9982bzgN5on1sLRmc1GMn6ZC.jpg",
            ThumbnailUrl = "https://image.tmdb.org/t/p/original/9HT9982bzgN5on1sLRmc1GMn6ZC.jpg",
            Id = i
        }).ToList();
    


        public TmdbService()
        {
        }
        

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return new List<Genre>();
        }

        public async Task<IEnumerable<Media>> GetTrendingAsync()
        {
            var fakes = _fakeMovies.ConvertAll(x => new Media()
            {
                Id = x.Id,
                DisplayTitle = $"Trending {x.Id}",
                MediaType = x.MediaType,
                Thumbnail = x.Thumbnail,
                ThumbnailSmall = x.ThumbnailSmall,
                ThumbnailUrl = x.ThumbnailUrl,
                Overview = x.Overview,
                ReleaseDate = x.ReleaseDate
            });
            return await Task.FromResult(fakes);
        }
        public async Task<IEnumerable<Media>> GetTopRatedAsync()
        {
            var fakes = _fakeMovies.ConvertAll(x => new Media()
            {
                Id = x.Id,
                DisplayTitle = $"Top Rated {x.Id}",
                MediaType = x.MediaType,
                Thumbnail = x.Thumbnail,
                ThumbnailSmall = x.ThumbnailSmall,
                ThumbnailUrl = x.ThumbnailUrl,
                Overview = x.Overview,
                ReleaseDate = x.ReleaseDate
            });
            return await Task.FromResult(fakes);
        }
        public async Task<IEnumerable<Media>> GetNetflixOriginalAsync()
        {
            var fakes = _fakeMovies.ConvertAll(x => new Media()
            {
                Id = x.Id,
                DisplayTitle = $"Original {x.Id}",
                MediaType = x.MediaType,
                Thumbnail = x.Thumbnail,
                ThumbnailSmall = x.ThumbnailSmall,
                ThumbnailUrl = x.ThumbnailUrl,
                Overview = x.Overview,
                ReleaseDate = x.ReleaseDate
            });
            return await Task.FromResult(fakes);
        }

        public async Task<IEnumerable<Media>> GetActionAsync()
        {
            var fakes = _fakeMovies.ConvertAll(x => new Media()
            {
                Id = x.Id,
                DisplayTitle = $"Action {x.Id}",
                MediaType = x.MediaType,
                Thumbnail = x.Thumbnail,
                ThumbnailSmall = x.ThumbnailSmall,
                ThumbnailUrl = x.ThumbnailUrl,
                Overview = x.Overview,
                ReleaseDate = x.ReleaseDate
            });
            return await Task.FromResult(fakes);
        }

        public async Task<IEnumerable<Video>?> GetTrailersAsync(int id, string type = "movie")
        {
            return null;
        }

        public async Task<MovieDetail> GetMediaDetailsAsync(int id, string type = "movie") =>
            new MovieDetail()
            {
                id = id,
                title = "Fake Movie 1",
                overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris commodo, massa a ultricies dapibus, mi tortor egestas sem, a accumsan elit tellus eget magna. Nulla facilisi. Suspendisse potenti. Nam ut aliquet velit. Curabitur eget risus auctor, sagittis libero quis, aliquet odio. Nulla facilisi. Nulla facilisi. Morbi sed libero quis ex cursus blandit. In hac habitasse platea dictumst. Maecenas ut nisl velit. Phasellus id nisl euismod, vehicula est a, suscipit nisl. Proin sit amet velit id risus sagittis convallis. Donec in tortor quis eros aliquet aliquet. Vivamus accumsan nisl eu tellus blandit, eget ultrices sapien ultricies. Fusce eget posuere ante, nec vulputate sapien.",
                release_date = "2021-01-01",
                poster_path = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/9HT9982bzgN5on1sLRmc1GMn6ZC.jpg",
                backdrop_path = "https://image.tmdb.org/t/p/original/9HT9982bzgN5on1sLRmc1GMn6ZC.jpg"
            };

        public async Task<IEnumerable<Media>> GetSimilarAsync(int id, string type = "movie") =>
            await Task.FromResult(_fakeMovies);

    }

    public class Movie
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result
    {
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; } // "movie" or "tv"
        public string ThumbnailPath => poster_path ?? backdrop_path;
        public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2/{ThumbnailPath}";
        public string ThumbnailSmall => $"https://image.tmdb.org/t/p/w220_and_h330_face/{ThumbnailPath}";
        public string ThumbnailUrl => $"https://image.tmdb.org/t/p/original/{ThumbnailPath}";
        public string DisplayTitle => title ?? name ?? original_title ?? original_name;

        public Media ToMediaObject() =>
            new ()
            {
                Id = id,
                DisplayTitle = DisplayTitle,
                MediaType = media_type,
                Overview = overview,
                ReleaseDate = release_date,
                Thumbnail = Thumbnail,
                ThumbnailSmall = ThumbnailSmall,
                ThumbnailUrl = ThumbnailUrl
            };
    }


    public class VideosWrapper
    {
        public int id { get; set; }
        public Video[] results { get; set; }

        public static Func<Video, bool> FilterTrailerTeasers => v =>
            v.official
            && v.site.Equals("Youtube", StringComparison.OrdinalIgnoreCase)
            && (v.type.Equals("Teaser", StringComparison.OrdinalIgnoreCase) || v.type.Equals("Trailer", StringComparison.OrdinalIgnoreCase));
    }

    public class Video
    {
        public string name { get; set; }
        public string key { get; set; }
        public string site { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
        public DateTime published_at { get; set; }
        public string Thumbnail => $"https://i.ytimg.com/vi/{key}/mqdefault.jpg";
    }


    public class MovieDetail
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }
    public class GenreWrapper
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
    public record struct Genre(int Id, string Name);
}
