// src/pages/CategoriesPage.tsx

import React, { useEffect, useState } from "react";
import CategoryCard from "../../components/Categorycard";
import { useNavigate } from "react-router-dom";
import Navbar from "../../components/Navbar";

interface Category {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
}

const CategoriesPage: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    // Static data
    const staticCategories: Category[] = [
      {
        id: 1,
        name: "Electronics",
        description: "All kinds of electronic devices",
        imageUrl:
          "https://i0.wp.com/onthesauceagain.com/wp-content/uploads/2024/01/rinck-content-studio-XwK1EyFAHJY-unsplash.jpg?resize=720%2C471&ssl=1",
      },
      {
        id: 2,
        name: "Furniture",
        description: "Furniture for your home and office",
        imageUrl:
          "https://www.bostonchefs.com/wp-content/uploads/2019/01/Collage.jpg",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
      {
        id: 3,
        name: "Books",
        description: "Books across various genres",
        imageUrl:
          "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRL7kx6NQeMgan-XnJOJgL3HOlqFsnuBIP0XQ&s",
      },
    ];

    // Set static data as the categories state
    setCategories(staticCategories);
  }, []);

  const handleCategoryClick = (categoryId: number) => {
    navigate(`/products/${categoryId}`);
  };

  return (
    <div>
      <Navbar />
      <div className="p-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {categories.map((category) => (
          <CategoryCard
            key={category.id}
            name={category.name}
            description={category.description}
            imageUrl={category.imageUrl}
            onClick={() => handleCategoryClick(category.id)}
          />
        ))}
      </div>
    </div>
  );
};

export default CategoriesPage;
