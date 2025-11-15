import { create } from 'zustand';
import { Movie, MovieDetails } from '../types/Movie';

interface MovieStore {
  movies: Movie[];
  selectedMovie: MovieDetails | null;
  loading: boolean;
  error: string | null;
  searchQuery: string;
  
  setMovies: (movies: Movie[]) => void;
  setSelectedMovie: (movie: MovieDetails | null) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string | null) => void;
  setSearchQuery: (query: string) => void;
}

export const useMovieStore = create<MovieStore>((set) => ({
  movies: [],
  selectedMovie: null,
  loading: false,
  error: null,
  searchQuery: '',
  
  setMovies: (movies) => set({ movies }),
  setSelectedMovie: (movie) => set({ selectedMovie: movie }),
  setLoading: (loading) => set({ loading }),
  setError: (error) => set({ error }),
  setSearchQuery: (query) => set({ searchQuery: query }),
}));