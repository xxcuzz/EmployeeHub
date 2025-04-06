const apiRequest = async (url: string, optionsObj?: RequestInit) => {
  try {
    const response = await fetch(url, optionsObj);
    
    if (!response.ok) {
      throw new Error(`Network response was not ok. Status: ${response.status}`);
    }

    return await response.json();
  } catch (e: any) {
    return { error: e.message };
  }
}

export default apiRequest;