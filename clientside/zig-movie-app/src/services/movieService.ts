import axios from 'axios';
import { Movie, MovieDetails } from '../types/Movie';

const API_BASE_URL = 'http://localhost:5000/api'; // Update with your server port
const IMAGE_BASE_URL = 'https://image.tmdb.org/t/p/w500';

export const movieService = {
  async getPopularMovies(): Promise<Movie[]> {
    try {
      const response = await axios.get<Movie[]>(`${API_BASE_URL}/popular`);
      return response.data;
    } catch (error) {
      console.error('Error fetching popular movies:', error);
      throw error;
    }
  },

  async searchMovies(query: string): Promise<Movie[]> {
    try {
      const response = await axios.get<Movie[]>(`${API_BASE_URL}/search`, {
        params: { query }
      });
      return response.data;
    } catch (error) {
      console.error('Error searching movies:', error);
      throw error;
    }
  },

  async getMovieById(id: number): Promise<MovieDetails> {
    try {
      const response = await axios.get<MovieDetails>(`${API_BASE_URL}/movie/${id}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching movie details:', error);
      throw error;
    }
  },

  getImageUrl(path: string | null | undefined): string {
    if (!path) {
      return 'https://via.placeholder.com/500x750?text=No+Image';
    }
    
    // If path is already a full URL, return it as is
    if (path.startsWith('http')) {
      return path;
    }
    
    // If path doesn't start with /, add it
    const imagePath = path.startsWith('/') ? path : `/${path}`;
    return `${IMAGE_BASE_URL}${imagePath}`;
  }
};