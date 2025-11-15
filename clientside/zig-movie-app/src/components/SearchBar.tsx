import React, { useState, useEffect } from 'react';
import { Form } from 'react-bootstrap';

interface SearchBarProps {
  onSearch: (query: string) => void;
  placeholder?: string;
}

export const SearchBar: React.FC<SearchBarProps> = ({ 
  onSearch, 
  placeholder = 'Search for movies...' 
}) => {
  const [query, setQuery] = useState('');

  // Debounce search
  useEffect(() => {
    const timer = setTimeout(() => {
      if (query.trim()) {
        onSearch(query);
      }
    }, 500);

    return () => clearTimeout(timer);
  }, [query]);

  return (
    <Form.Group className="mb-4">
      <Form.Control
        type="text"
        placeholder={placeholder}
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        size="lg"
      />
    </Form.Group>
  );
};