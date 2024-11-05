// src/components/CategoryCard.tsx

import React from 'react';

interface CategoryCardProps {
  name: string;
  description: string;
  imageUrl: string;
  onClick: () => void;
}

const CategoryCard: React.FC<CategoryCardProps> = ({ name, description, imageUrl, onClick }) => {
  return (
    <div
      className="bg-blue-100 p-4 rounded shadow-md hover:bg-blue-200 transition cursor-pointer flex items-center space-x-4"
      onClick={onClick}
    >
      <img 
        src={imageUrl} 
        alt={name} 
        className="w-16 h-16 rounded object-cover" 
      />
      <div>
        <h3 className="text-xl font-semibold text-gray-800">{name}</h3>
        <p className="text-gray-600 text-sm">{description}</p>
      </div>
    </div>
  );
};

export default CategoryCard;
