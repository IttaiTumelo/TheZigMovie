import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Button, Badge, Alert } from 'react-bootstrap';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { movieService } from '../services/movieService';
import { MovieDetails } from '../types/Movie';

export const DetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [movie, setMovie] = useState<MovieDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (id) {
      loadMovieDetails(parseInt(id));
    }
  }, [id]);

  const loadMovieDetails = async (movieId: number) => {
    setLoading(true);
    setError(null);
    try {
      const data = await movieService.getMovieById(movieId);
      setMovie(data);
    } catch (err) {
      setError('Failed to load movie details. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <LoadingSpinner />;
  if (error) return <Alert variant="danger">{error}</Alert>;
  if (!movie) return <Alert variant="warning">Movie not found.</Alert>;

  return (
    <Container className="py-5">
      <Button variant="secondary" onClick={() => navigate(-1)} className="mb-4">
        ← Back
      </Button>

      <Row>
        <Col md={4}>
          <img
            src={movieService.getImageUrl(movie.poster_path)}
            alt={movie.title}
            className="img-fluid rounded shadow"
          />
        </Col>

        <Col md={8}>
          <h1>
            {movie.homepage ? (
              <a href={movie.homepage} target="_blank" rel="noopener noreferrer">
                {movie.title}
              </a>
            ) : (
              movie.title
            )}
          </h1>

          {movie.tagline && (
            <p className="text-muted fst-italic">"{movie.tagline}"</p>
          )}

          <div className="mb-3">
            {movie.genres && movie.genres.map((genre) => (
              <Badge bg="primary" className="me-2" key={genre.id}>
                {genre.name}
              </Badge>
            ))}
          </div>

          <div className="mb-3">
            <strong>Rating:</strong> {movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A'} / 10 
            ({movie.vote_count || 0} votes)
          </div>

          <div className="mb-3">
            <strong>Release Date:</strong> 📅 {movie.release_date}
          </div>

          {movie.runtime > 0 && (
            <div className="mb-3">
              <strong>Runtime:</strong> ⏱️ {movie.runtime} minutes
            </div>
          )}

          <div className="mb-4">
            <h5>Overview</h5>
            <p>{movie.overview}</p>
          </div>

          {movie.production_companies && movie.production_companies.length > 0 && (
            <div>
              <h5>Production Companies</h5>
              <ul>
                {movie.production_companies.map((company) => (
                  <li key={company.id}>{company.name}</li>
                ))}
              </ul>
            </div>
          )}
        </Col>
      </Row>
    </Container>
  );
};