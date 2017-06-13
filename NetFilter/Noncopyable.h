#pragma once
class Noncopyable {
private:
	Noncopyable(const Noncopyable&) {};
	Noncopyable& operator=(const Noncopyable&) {};
public:
	Noncopyable() {};
	virtual ~Noncopyable() {};
};

