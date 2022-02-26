import React from 'react';
import _ from 'lodash';

type PaginationPropTypes = {
  itemsCount: number;
  pageSize: number;
  currentPage: number;
  onPageChange: (page: number) => void;
};

function Pagination({
  itemsCount,
  pageSize,
  currentPage,
  onPageChange,
}: PaginationPropTypes) {
  const pagesCount = Math.ceil(itemsCount / pageSize);
  if (pagesCount === 1) return null;
  const pages = _.range(1, pagesCount + 1);

  return (
    <nav>
      <ul className="pagination">
        {pages.map((page) => (
          <li
            key={page}
            className={page === currentPage ? 'page-item active' : 'page-item'}
          >
            <span
              className="page-link"
              role="button"
              aria-hidden
              onClick={() => onPageChange(page)}
            >
              {page}
            </span>
          </li>
        ))}
      </ul>
    </nav>
  );
}

export default Pagination;
