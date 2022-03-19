import { faBasketball } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';

function Loading() {
  return (
    <FontAwesomeIcon className="d-flex mx-auto" icon={faBasketball} spin />
  );
}

export default Loading;
