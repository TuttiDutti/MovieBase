export type MovieListDto = {
    tmdbId: number;
    title: string;
    posterUrl: string;
    genres: string[];
};

export type MovieDto = {
    tmdbId: number;
    title: string;
    posterUrl: string;
    releaseDate: string;
    genres: string[];
    description: string;
    runtime: number;
}