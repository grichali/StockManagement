import React, { useState, useEffect } from "react";

function Products() {
  interface Product {
    id: number;
    name: string;
    price: number;
    quantity: number;
    imageUrl: string;
    categoryDto?: CategoryDto | null;
  }

  interface CategoryDto {
    id: number;
    name: string;
    description: string;
    imageUrl: string;
  }

  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_API_URL}/api/Product/getall`
        );
        if (!response.ok) {
          throw new Error(`Failed to fetch Products: ${response.statusText}`);
        }

        const data = await response.json();
        console.log(data.$values);
        setProducts(data.$values);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  if (loading) {
    return <div>Loading categories...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="bg-gray-100">
      <div className="header bg-white h-16 px-10 py-8 border-b-2 border-gray-200 flex items-center justify-between">
        <div className="flex items-center space-x-2 text-gray-400">
          <span className="text-green-7s00 tracking-wider font-thin text-md">
            <a href="/">Home</a>
          </span>
          <span>/</span>
          <span className="tracking-wide text-md">
            <span className="text-base">Products</span>
          </span>
          <span>/</span>
        </div>
      </div>
      <div className="header my-3 h-12 px-10 flex items-center justify-between">
        <h1 className="font-medium text-2xl">All Products</h1>
      </div>
      <div className="flex flex-col mx-3 mt-6 lg:flex-row">
        <div className="w-full lg:w-1/4 m-1">
          <form className="w-full bg-white shadow-md p-6">
            <div className="flex flex-wrap -mx-3 mb-6">
              <div className="w-full md:w-full px-3 mb-6">
                <label
                  className="block uppercase tracking-wide text-gray-700 text-sm font-bold mb-2"
                  htmlFor="category_name"
                >
                  Products Name
                </label>
                <input
                  className="appearance-none block w-full bg-white text-gray-900 font-medium border border-gray-400 rounded-lg py-3 px-3 leading-tight focus:outline-none focus:border-[#98c01d]"
                  type="text"
                  name="name"
                  placeholder="Category Name"
                  required
                />
              </div>
              <div className="w-full px-3 mb-6">
                <textarea
                  className="appearance-none block w-full bg-white text-gray-900 font-medium border border-gray-400 rounded-lg py-3 px-3 leading-tight focus:outline-none focus:border-[#98c01d]"
                  name="description"
                  placeholder="Description"
                  required
                ></textarea>
              </div>
              <div className="w-full md:w-full px-3 mb-6">
                <button className="appearance-none block w-full bg-green-700 text-gray-100 font-bold border border-gray-200 rounded-lg py-3 px-3 leading-tight hover:bg-green-600 focus:outline-none focus:bg-white focus:border-gray-500">
                  Add Products
                </button>
              </div>
              <div className="w-full px-3 mb-8">
                <label
                  className="mx-auto cursor-pointer flex w-full max-w-lg flex-col items-center justify-center rounded-xl border-2 border-dashed border-green-400 bg-white p-6 text-center"
                  htmlFor="dropzone-file"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-10 w-10 text-green-800"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    strokeWidth="2"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12"
                    />
                  </svg>
                  <h2 className="mt-4 text-xl font-medium text-gray-700 tracking-wide">
                    Products image
                  </h2>
                  <p className="mt-2 text-gray-500 tracking-wide">
                    Upload or drag & drop your file SVG, PNG, JPG or GIF.
                  </p>
                  <input
                    id="dropzone-file"
                    type="file"
                    className="hidden"
                    name="category_image"
                    accept="image/png, image/jpeg, image/webp"
                  />
                </label>
              </div>
            </div>
          </form>
        </div>
        <div className="w-full lg:w-3/4 m-1 bg-white shadow-lg text-lg rounded-sm border border-gray-200">
          <div className="overflow-x-auto rounded-lg p-3">
            <table className="table-auto w-">
              <thead className="text-sm font-semibold uppercase text-gray-800 bg-gray-50 mx-auto">
                <tr>
                  <th></th>
                  <th>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      className="fill-current w-5 h-5 mx-auto"
                    >
                      <path d="M6 22h12a2 2 0 0 0 2-2V8l-6-6H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2zm7-18 5 5h-5V4zm-4.5 7a1.5 1.5 0 1 1-.001 3.001A1.5 1.5 0 0 1 8.5 11zm.5 5 1.597 1.363L13 13l4 6H7l2-3z"></path>
                    </svg>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold">Product</div>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold text-left">Price</div>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold text-center">Quantity</div>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold text-center">Category</div>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold text-center">Status</div>
                  </th>
                  <th className="px-10 py-2">
                    <div className="font-semibold text-center">Action</div>
                  </th>
                </tr>
                {products.map((product, i) => (
                  <tr>
                    <td>{product.id}</td>
                    <td>
                      <img
                        src="https://images.pexels.com/photos/25652584/pexels-photo-25652584/free-photo-of-elegant-man-wearing-navy-suit.jpeg?auto=compress&cs=tinysrgb&w=400"
                        className="h-8 w-8 mx-auto"
                        alt="Category"
                      />
                    </td>
                    <td>{product.name}</td>
                    <td>{product.price}</td>
                    <td>{product.quantity}</td>
                    <td>{product.categoryDto?.name}</td>
                    <td className="text-green-500">in stock</td>
                    <td className="p-2">
                      <div className="flex justify-center">
                        <a
                          href="/"
                          className="rounded-md hover:bg-green-100 text-green-600 p-2 flex justify-between items-center"
                        >
                          Edit
                        </a>
                        <button className="rounded-md hover:bg-red-100 text-red-600 p-2 flex justify-between items-center">
                          Delete
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </thead>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Products;
