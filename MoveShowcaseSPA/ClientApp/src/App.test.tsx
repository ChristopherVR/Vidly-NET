import React from 'react';
import { render } from '@testing-library/react';
import App from './App';

test('should throw error when not wrapped inside `BrowserRouter`', () => {
  expect(() => render(<App />)).toThrow(
    'useRoutes() may be used only in the context of a <Router> component.',
  );
});
