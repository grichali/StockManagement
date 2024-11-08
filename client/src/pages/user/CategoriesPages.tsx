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
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        // Fetch categories from API
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Category/getall`);
        if (!response.ok) {
          throw new Error(`Failed to fetch categories: ${response.statusText}`);
        }

        const data = await response.json();
        console.log(data.$values)
        setCategories(data.$values);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  const handleCategoryClick = (category: Category) => {
    navigate(`/user/produits/${category.id}`, {
      state: {
        categoryName: category.name,
        categoryDescription: category.description,
      },
    });
  };

  if (loading) {
    return <div>Loading categories...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

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
            onClick={() => handleCategoryClick(category)}
          />
        ))}
      </div>
    </div>
  );
};

export default CategoriesPage;
