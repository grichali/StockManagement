import React from 'react';

const Navbar: React.FC = () => {
    const userName = "John Doe"; 
    const cartItemCount = 3; 

    return (
        <nav className="flex items-center justify-between p-4 bg-blue-600 text-white">
            <div className="text-lg font-semibold">ShopApp</div>
            
            <div className="flex items-center gap-4">
                <span className="hidden sm:inline">Hello, {userName}</span>
                
                <div className="relative">
                    <button className="text-white">
                        {/* Cart Icon */}
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13l-1.2 6.3a1 1 0 001 1.2h10a1 1 0 001-1.2L17 13M7 13H5M15 6a2 2 0 100-4 2 2 0 000 4z" />
                        </svg>
                    </button>
                    {/* Badge for item count */}
                    <span className="absolute -top-2 -right-2 bg-red-500 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center">
                        {cartItemCount}
                    </span>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
