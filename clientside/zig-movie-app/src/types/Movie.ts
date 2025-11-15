export interface Movie {
  id: number;
  title: string;
  overview: string;
  poster_path: string;
  backdrop_path: string;
  popularity: number;
  vote_average: number;
  vote_count: number;
  release_date: string;
  adult: boolean;
  genre_ids: number[];
  original_language: string;
  original_title: string;
}

export interface MovieDetails extends Movie {
  homepage: string;
  budget: number;
  revenue: number;
  runtime: number;
  status: string;
  tagline: string;
  genres: Genre[];
  production_companies: ProductionCompany[];
}

export interface Genre {
  id: number;
  name: string;
}

export interface ProductionCompany {
  id: number;
  name: string;
  logo_path: string;
  origin_country: string;
}