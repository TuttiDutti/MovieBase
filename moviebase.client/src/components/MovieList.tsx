import React, { useEffect, useState } from "react";
import { MovieListDto } from "../types"; 

const MovieList = () => {
    const [movies, setMovies] = useState<MovieListDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);

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

    if (isLoading) return <p>£adowanie filmów...</p>;

    console.log(movies);
    return (
        <div>
            <h1>Lista filmów</h1>
            
            <ul>
                {movies.map((movie) => (
                    <li key={movie.tmdbId}>
                        <img src={movie.posterUrl} alt={movie.title} width={100} />
                        <div>{movie.title}</div>
                        <div>{movie.releaseDate}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MovieList;