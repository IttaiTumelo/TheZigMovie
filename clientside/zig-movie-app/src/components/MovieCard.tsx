import React from 'react';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { Movie } from '../types/Movie';
import { movieService } from '../services/movieService';

interface MovieCardProps {
  movie: Movie;
}

export const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
  return (
    <Card className="h-100 shadow-sm">
      <Card.Img 
        variant="top" 
        src={movieService.getImageUrl(movie.poster_path)} 
        alt={movie.title}
        style={{ height: '400px', objectFit: 'cover' }}
      />
      <Card.Body>
        <Card.Title>
          <Link 
            to={`/movie/${movie.id}`} 
            className="text-decoration-none text-dark"
          >
            {movie.title}
          </Link>
        </Card.Title>
        <Card.Text className="text-muted small">
          {movie.overview ? movie.overview.substring(0, 100) + '...' : 'No description available'}
        </Card.Text>
        <div className="d-flex justify-content-between align-items-center mt-2">
          <small className="text-muted">
            ⭐ {movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A'}
          </small>
          <small className="text-muted">
            {movie.release_date ? new Date(movie.release_date).getFullYear() : 'N/A'}
          </small>
        </div>
      </Card.Body>
    </Card>
  );
};