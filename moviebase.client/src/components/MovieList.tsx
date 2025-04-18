import React, { useEffect, useState } from "react";
import { MovieListDto } from "../types"; 

const MovieList = () => {
    const [movies, setMovies] = useState<MovieListDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [searchQuery, setSearchQuery] = useState("");

    useEffect(() => {
       
        const fetchMovies = async () => {
            try {
                const response = await fetch("/api/movie/list");
                if (!response.ok) {
                    const text = await response.text();
                    console.error("?? Backend zwróci³ b³¹d:", response.status, text);
                    return;
                }
                const data = await response.json();

                console.log("Response z backendu:", data);
                setMovies(data); 
            } catch (error) {
                console.error("B³¹d podczas pobierania filmów", error);
            } finally {
                setIsLoading(false);
            }
        };

        fetchMovies();
    }, []);
    const handleSearch = async () => {
        try {
            const response = await fetch(`/api/movie/list?q=${encodeURIComponent(searchQuery)}`);
            if (!response.ok) throw new Error("Search failed");

            const data = await response.json();
            setMovies(data);
        } catch (err) {
            console.error("Error searching movies:", err);
        }
    };

    if (isLoading) return <p>Loading...</p>;

    return (
        <div>
            <h1 className="title">Movie list</h1>
            <div className="search">
                <input className="searchbar" type="text" placeholder="Search" value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} onKeyDown={(e) => {if (e.key === "Enter") handleSearch();}}></input>
                <button className="search-icon" onClick={handleSearch}>
                    <span class="material-symbols-outlined">search</span>
                </button>
                
                
            </div>

            <ul className="movie-grid">
                {movies.map((movie) => (
                    <li key={movie.tmdbId} className="movie-card">
                        <img src={movie.posterUrl || "/no-img.png"} alt={movie.title} width={100} className="movie-poster" />
                        <div className="movie-title">{movie.title}</div>
                        <div className="movie-genres">{movie.genres.slice(0, 2).map((genre, index) => (
                            <span key={index}>
                                {genre}
                                {index < 1 && movie.genres.length > 1 && ', '}
                            </span>))}
                        </div>
                        
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MovieList;