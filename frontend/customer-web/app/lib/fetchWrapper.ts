import { getTokenWorkaround } from "@/app/actions/authActions"

const baseUrl = 'http://localhost:6001/'

export const fetchWrapper = { get, post, put, del }

async function get(url: string) {
  const requestOptions = {
    method: 'GET',
    headers: await getHeaders()
  }
  return fetchData(url, requestOptions)
}

async function post(url: string, body: {}) {
  const requestOptions = {
    method: 'POST',
    headers: await getHeaders(),
    body: JSON.stringify(body)
  }
  return fetchData(url, requestOptions)
}

async function put(url: string, body: {}) {
  const requestOptions = {
    method: 'PUT',
    headers: await getHeaders(),
    body: JSON.stringify(body)
  }
  return fetchData(url, requestOptions)
}

async function del(url: string) {
  const requestOptions = {
    method: 'DELETE',
    headers: await getHeaders()
  }
  return fetchData(url, requestOptions)
}

async function getHeaders() {
  const token = await getTokenWorkaround()
  const headers = { 'Content-Type': 'application/json' } as any

  if (token) headers.Authorization = 'Bearer ' + token.access_token
  
  return headers
}

async function fetchData(url: string, requestOptions: RequestInit) {
  const response = await fetch(baseUrl + url, requestOptions)
  return await handleResponse(response)
}

async function handleResponse(response: Response) {
  const text = await response.text()
  
  let data
  try {
    data = JSON.parse(text)
  } catch (error) {
    data = text
  }

  if (response.ok) {
    return data || response.statusText
  } else {
    const error = {
      status: response.status,
      message: typeof data === 'string' ? data : response.statusText
    }
    return {error};
  }
}
