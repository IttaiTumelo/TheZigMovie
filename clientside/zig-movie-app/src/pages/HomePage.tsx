import React, { useCallback, useEffect, useState } from 'react';
import { Container, Row, Col, Alert } from 'react-bootstrap';
import { SearchBar } from '../components/SearchBar';
import { MovieCard } from '../components/MovieCard';
import { LoadingSpinner } from '../components/LoadingSpinner';
import { useMovieStore } from '../store/movieStore';
import { movieService } from '../services/movieService';

export const HomePage: React.FC = () => {
  const { movies, loading, error, setMovies, setLoading, setError } = useMovieStore();
  const [isSearching, setIsSearching] = useState(false);

  const loadPopularMovies = async () => {
    setLoading(true);
    setError(null);
    setIsSearching(false);
    try {
      const data = await movieService.getPopularMovies();
      setMovies(data);
    } catch (err) {
      setError('Failed to load popular movies. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadPopularMovies();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSearch = useCallback( async (query: string) => {
    if (!query.trim()) {
      loadPopularMovies();
      return;
    }

    setLoading(true);
    setError(null);
    setIsSearching(true);
    try {
      const data = await movieService.searchMovies(query);
      setMovies(data);
      console.log('Search results for query:', query, data);
    } catch (err) {
      setError('Failed to search movies. Please try again.');
    } finally {
      setLoading(false);
    }
  },[])

  return (
    <Container className="py-5">
      <h1 className="text-center mb-4">
        {isSearching ? 'Search Results' : 'Popular Movies'}
      </h1>
      
      <SearchBar onSearch={handleSearch} />

      {error && (
        <Alert variant="danger" dismissible onClose={() => setError(null)}>
          {error}
        </Alert>
      )}

      {loading ? (
        <LoadingSpinner />
      ) : (
        <>
          {movies.length === 0 ? (
            <Alert variant="info">No movies found.</Alert>
          ) : (
            <Row xs={1} sm={2} md={3} lg={4} className="g-4">
              {movies.map((movie) => (
                <Col key={movie.id}>
                  <MovieCard movie={movie} />
                </Col>
              ))}
            </Row>
          )}
        </>
      )}
    </Container>
  );
};