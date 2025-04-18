import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { MovieDto } from "../types";

const MovieDetails = () => {
    const { id } = useParams();
    const [movie, setMovie] = useState<MovieDto>();
    const [isLoading, setIsLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchMovie = async () => {
            try {
                const response = await fetch(`/api/movie/${id}`);
                if (!response.ok) {
                    const text = await response.text();
                    console.error("Backend error:", response.status, text);
                    return;
                }
                const data = await response.json();
                setMovie(data);
            } catch (error) {
                console.error("Error occured: ", error)
            } finally {
                setIsLoading(false);
            }
        };
        fetchMovie();
    }, []);

    if (isLoading) return <p>Loading...</p>;
    if (!movie) return <p>Loading...</p>;

    return (
        <div className="page">
            <div className="headerRow">
                <div className="movieTitle">
                    <h1 className="title" style={{ color: "#F25F5C" }}>{movie.title}</h1>
                </div>
                <button className="back" onClick={() => navigate("/") }>
                    <span class="material-symbols-outlined">arrow_back</span>
                    <span className="back-text">Go back</span>
                </button>
                
            </div>
            <div className="movie-details">
                <img src={movie.posterUrl || "/no-img.png"} alt={movie.title} width={100} className="poster-movie" />
                <div style={{ textAlign: "left" }}>
                    <h3 style={{ margin: 0 }}>Genre: </h3>
                    <h4 className="genres-movie" style={{ margin: 0 }}>{movie.genres.slice(0, 2).map((genre, index) => (
                        <span key={index}>
                            {genre}
                            {index < 1 && movie.genres.length > 1 && ', '}
                        </span>))}
                    </h4>
                    <h3 className="desc">Description: </h3>
                    <div className="description">{movie.description}</div>
                    <h3 className="desc">Release date: </h3>
                    <div className="releaseDate">{movie.releaseDate}</div>
                    <h3 className="desc">Runtime: </h3>
                    <div className="runtime">{movie.runtime} min.</div>
                </div>
                
            </div>
            
        </div>
    )
};

export default MovieDetails;
